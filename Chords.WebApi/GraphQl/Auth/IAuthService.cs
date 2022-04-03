using System.Threading.Tasks;
using Chords.DataAccess.EntityFramework;
using Chords.DataAccess.Models;
using Chords.Web.GraphQl.Auth;

namespace Chords.WebApi.GraphQl.Auth
{
    public interface IAuthService
    {
        Task<Token> Login(LoginInput loginInput);
        Task<Token> Register(RegisterInput registerInput);
    }

}