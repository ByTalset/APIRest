using Practicas.Modelos;

namespace Practicas.Datos
{
    public static class dboPersona
    {
        public static List<Persona> personas = new List<Persona>()
        {
                new Persona
                {
                    id = 1,
                    name = "Antonio",
                    apellidoPaterno = "López",
                    apellidoMaterno = "Castillo",
                    edad = 25,
                    salario = 30850
                },
                new Persona
                {
                    id = 2,
                    name = "Axel Roberto",
                    apellidoPaterno = "Marcial",
                    apellidoMaterno = "Gomez",
                    edad = 23,
                    salario = 30850
                },
                new Persona
                {
                    id = 3,
                    name = "Adolfo",
                    apellidoPaterno = "Mendez",
                    apellidoMaterno = "Ruiz",
                    edad = 26,
                    salario = 20000
                },
                new Persona
                {
                    id = 4,
                    name = "Carlos",
                    apellidoPaterno = "Granados",
                    apellidoMaterno = "Gonzalez",
                    edad = 23,
                    salario = 30850
                },
                new Persona
                {
                    id = 5,
                    name = "Jessica",
                    apellidoPaterno = "Castillo",
                    apellidoMaterno = "Ramos",
                    edad = 45,
                    salario = 15000
                },
                new Persona
                {
                    id = 6,
                    name = "Dante Arturo",
                    apellidoPaterno = "Bautista",
                    apellidoMaterno = "Cruz",
                    edad = 28,
                    salario = 18000
                }
        };
    }
}
