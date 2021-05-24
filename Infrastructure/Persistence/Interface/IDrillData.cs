using System.Threading.Tasks;

namespace Infrastructure.Persistence.Interface
{
    public interface IDrillData
    {
        Task DrillData(string fileType);
    }
}