using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Practicas.Datos;
using Practicas.Modelos;
using Practicas.Modelos.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Practicas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoggingController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public LoggingController(ILogger<LoggingController> logger, IMapper mapper, IConfiguration config)
        {
            _logger = logger;
            _mapper = mapper;
            _config = config;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<PersonaDto>> ConsultaPersonas()
        {
            //var jwt = _config.GetSection("Jwt").Get<JwtSettings>();

            _logger.LogInformation("Esta es una informacion relevante");
            _logger.LogWarning("¡Cuidado esto es una advertencia!");
            _logger.LogError("Se produjo un error");
            var personas2 = _mapper.Map<List<PersonaDto>>(dboPersona.personas);
            return Ok(personas2);
        }

        [HttpGet("{id}", Name = "ConsultaPersona")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PersonaDto> ConsultaPersona(int id)
        {
            _logger.LogInformation("Esta es una informacion relevante");

            if (id == 0)
            {
                _logger.LogWarning("¡Cuidado esto es una advertencia!");
                return BadRequest();
            }

            Persona persona = dboPersona.personas.Find(x => x.id == id);
            var persona2 = _mapper.Map<PersonaDto>(persona);

            if (persona2 == null)
            {
                _logger.LogError("Se produjo un error");
                return NotFound(persona2);
            }

            return Ok(persona2);
        }

        [HttpPost]
        [Route("Crea")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PersonaDto> CreaPersona([FromBody] Persona persona)
        {

            if (persona == null)
            {
                return BadRequest(persona);
            }
            else if (persona.id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            persona.id = dboPersona.personas.OrderByDescending(v => v.id).FirstOrDefault().id + 1;
            dboPersona.personas.Add(persona);
            PersonaDto personaDto = _mapper.Map<PersonaDto>(dboPersona.personas.Find(x => x.id == persona.id));
            return CreatedAtRoute("ConsultaPersona", new { id = personaDto.id }, personaDto);
        }

        [HttpDelete]
        [Route("Elimina")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult EliminaPersona(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            Persona persona = dboPersona.personas.Find(x => x.id == id);
            if (persona == null)
            {
                return NotFound(persona);
            }

            dboPersona.personas.Remove(persona);
            return NoContent();
        }

        [HttpPut]
        [Route("Actualiza")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ActualizarPersona(int id, [FromBody] Persona persona)
        {
            if (id == 0 || persona == null)
            {
                return BadRequest();
            }
            Persona personaPut = dboPersona.personas.Find(x => x.id == id);
            _mapper.Map(persona, personaPut);
            return NoContent();
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> InicioSesion([FromBody] SolicitudUsers users)
        {
            var jwt = _config.GetSection("Jwt").Get<JwtSettings>();
            if (users.username.Equals("Talset") && users.password.Equals("Libra.1997"))
            {
                var claims = new List<Claim>();

                claims.Add(new Claim(ClaimTypes.Name, users.username));
                claims.Add(new Claim(ClaimTypes.Role, users.roll));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.key));
                var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = credenciales
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new
                {
                    Mensaje = "Usuario autenticado",
                    Token = tokenHandler.WriteToken(token)
                });

            }
            return BadRequest("Usuario no se autentico");
        }
    }
}
