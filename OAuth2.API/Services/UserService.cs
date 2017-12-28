using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OAuth2.Data.Authentication.Models;
using OAuth2.API.Models;
using OAuth2.API.Repositories;

namespace OAuth2.API.Services
{
    public class UserService
    {
        private DBAuthentication _repo;
        public UserService()
        {
            _repo = new DBAuthentication();
        }
        public async Task<bool> Register(UserModel user)
        {
            var result = await _repo.QueryAsync<int>("SELECT COUNT(1) FROM dbo.[Users] WHERE UserName = @UserName",
                new { UserName = user.UserName });
            if (result.First() > 0)
                throw new ArgumentException("user has existed");
            else
                return await _repo.ExecuteAsync("INSERT INTO dbo.[Users] VALUES(@UserId, @UserName, @Password)", new { UserId = Guid.NewGuid(), UserName = user.UserName, Password = user.Password });
        }

        public async Task<User> Login(string userName, string password)
        {
            var result = await _repo.QueryAsync<User>("SELECT * FROM dbo.[Users] WHERE UserName = @UserName AND Password = @Password",
                new { UserName = userName, Password = password });
            if (result.Count() < 0)
                throw new ArgumentException("bad username or password");
            else
                return result.FirstOrDefault();
        }

        public async Task<Client> FindClient(string clientId)
        {
            var clients = await _repo.QueryAsync<Client>("SELECT * FROM dbo.Clients WHERE Id = @ClientId",
                new { ClientId = clientId });

            return clients.FirstOrDefault();
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {
            var tokens = await _repo.QueryAsync<RefreshToken>("SELECT * FROM dbo.RefreshTokens WHERE [Identity]=@Identity AND ClientId=@ClientId",
                new { Identity = token.Identity, ClientId = token.ClientId });

            var existingToken = tokens.FirstOrDefault();
            if (existingToken != null)
            {
                var result = await RemoveRefreshToken(existingToken);
            }

            return await _repo.ExecuteAsync("INSERT INTO dbo.RefreshTokens VALUES(@Id, @Identity, @ClientId, @IssuedUtc, @ExpiresUtc, @ProtectedTicket)",
                new
                {
                    Id = token.Id,
                    Identity = token.Identity,
                    ClientId = token.ClientId,
                    IssuedUtc = token.IssuedUtc,
                    ExpiresUtc = token.ExpiresUtc,
                    ProtectedTicket = token.ProtectedTicket
                });
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            return await _repo.ExecuteAsync("DELETE FROM dbo.RefreshTokens WHERE Id = @RefreshTokenId",
                new { RefreshTokenId = refreshTokenId });
        }


        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            return await _repo.ExecuteAsync("DELETE FROM dbo.RefreshTokens WHERE ClientId = @ClientID AND [Identity] = @Identity",
                new { Identity = refreshToken.Identity, ClientId = refreshToken.ClientId });
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var tokens = await _repo.QueryAsync<RefreshToken>("SELECT * FROM dbo.RefreshTokens WHERE Id = @RefreshTokenId", new { RefreshTokenId = refreshTokenId });
            return tokens.FirstOrDefault();
        }

        public async Task<IEnumerable<RefreshToken>> GetAllRefreshTokens()
        {
            return await _repo.QueryAsync<RefreshToken>("SELECT * FROM dbo.RefreshTokens");
        }
    }
}