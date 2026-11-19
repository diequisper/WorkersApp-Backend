using WorkersApp.Models;
using WorkersApp.Repository;

namespace WorkersApp.Services
{
    public class TrabajadorService
    {
        private readonly TrabajadorRepository _trabajadorRepository;

        public TrabajadorService(TrabajadorRepository trabajadorRepository)
        {
            _trabajadorRepository = trabajadorRepository;
        }

        public async Task<List<Trabajador>> GetById(string? dni)
        {
            List<Trabajador> repoResult = await _trabajadorRepository.GetById(dni);
            return repoResult;
        }

        public async Task<GenericServiceResponse<object>> CreateTrabajador(Trabajador trabajador)
        {
            GenericServiceResponse<object> serviceResponse = new GenericServiceResponse<object>
            {
                Status = GenericServiceResponse<object>.Result.InvalidData
            };

            switch(trabajador)
            {
                case { Nombre: string n } when string.IsNullOrWhiteSpace(n):
                    serviceResponse.Response = "El campo Nombre es inválido";
                    return serviceResponse;
                case { Apellido: string a } when string.IsNullOrWhiteSpace(a):
                    serviceResponse.Response = "El campo Apellido es inválido";
                    return serviceResponse;
                case { Dni: string d } when string.IsNullOrWhiteSpace(d) || d.Length != 8 || !d.All(char.IsDigit):
                    serviceResponse.Response = d switch
                    {
                        _ when string.IsNullOrWhiteSpace(d) => "El campo DNI es inválido",
                        _ when d.Length != 8 => "El número de DNI debe tener 8 caracteres",
                        _ => "El número de DNI solo debe contener números"
                    };

                    return serviceResponse;
                case { Edad: int e } when e <= 14 || e > 90:
                    serviceResponse.Response = e switch
                    {
                        <= 14 => "La edad del trabajador debe ser mayor a 14 años",
                        > 90 => "La edad del trabajador supera el límite válido",
                        _ => "Error en el campo edad"
                    };
                    return serviceResponse;
                default:
                    break;
            }

            bool isSaved = await _trabajadorRepository.save(trabajador);
            if (isSaved == false)
            {
                serviceResponse.Status = GenericServiceResponse<object>.Result.OperationFailed;
                serviceResponse.Response = "No se ha podido registrar al trabajador";
                return serviceResponse;
            };

            serviceResponse.Status = GenericServiceResponse<object>.Result.Success;
            serviceResponse.Response = "Trabajador registrado";
            return serviceResponse;
        }
    }
}
