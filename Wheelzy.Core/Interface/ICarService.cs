using Wheelzy.Core.DTO;
using Wheelzy.Core.Services;

namespace Wheelzy.Core.Interface;
public interface ICarService {
    Task<List<CarInfoDTO>> GetCarDetailsAsync();
    bool IsDatabaseConnected();
}