
using ChequeAPI.Models;

namespace ChequeAPI.Services
{
    public interface IChequeService
    {
        byte[] GenerateCheque(ChequeDTO cheque);
    }
}
