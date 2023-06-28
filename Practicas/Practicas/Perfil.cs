using AutoMapper;
using Practicas.Modelos;
using Practicas.Modelos.Dto;

namespace Practicas
{
    public class Perfil : Profile
    {
        public Perfil() 
        {
            CreateMap<Persona, PersonaDto>()
                .ForMember(dto => dto.nombreCompleto, p => p.MapFrom(src => $"{src.name} {src.apellidoPaterno} {src.apellidoMaterno}"));
            CreateMap<Persona, Persona>()
                .ForMember(p => p.id, ps => ps.Ignore())
                .ForMember(p => p.salario, ps => ps.Ignore());
        }
    }
}
