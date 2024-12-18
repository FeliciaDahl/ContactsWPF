

using Business.Interfaces;

namespace Business.Helpers;

public class GenerateUniqeId : IGenerateUniqeId
{
    public string GenerateId()
    {
        return Guid.NewGuid().ToString();
    }
}
