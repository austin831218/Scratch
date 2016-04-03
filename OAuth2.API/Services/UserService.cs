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
            var result = await _repo.QueryAsync<int>("SELECT COUNT(1) FROM dbo.[User] WHERE UserName = @UserName",
                new { UserName = user.UserName });
            if (result.First() > 0)
                throw new ArgumentException("user has existed");
            else
                return await _repo.ExecuteAsync("INSERT INTO dbo.[User] VALUES(@UserName, @Password)", new { UserName = user.UserName, Password = user.Password });
        }

        public async Task<AuthenticatedUser> Login(string userName, string password)
        {
            var result = await _repo.QueryAsync<AuthenticatedUser>("SELECT * FROM dbo.[User] WHERE UserName = @UserName AND Password = @Password",
                new { UserName = userName, Password = password });
            if (result.Count() < 0)
                throw new ArgumentException("bad username or password");
            else
                return result.FirstOrDefault();
        }

        public async Task<Client> FindClient(string clientId)
        {
            var clients = await _repo.QueryAsync<Client>("SELECT * FROM dbo.Client WHERE Id = @ClientId",
                new { ClientId = clientId });

            return clients.FirstOrDefault();
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {
            var tokens = await _repo.QueryAsync<RefreshToken>("SELECT * FROM dbo.RefreshToken WHERE Subject=@Subject AND ClientId=@ClientId",
                new { Subject = token.Subject, ClientId = token.ClientId });

            var existingToken = tokens.FirstOrDefault();
            if (existingToken != null)
            {
                var result = await RemoveRefreshToken(existingToken);
            }

            return await _repo.ExecuteAsync("INSERT INTO dbo.RefreshToken VALUES(@Id, @Subject, @ClientId, @IssuedUtc, @ExpiresUtc, @ProtectedTicket)",
                new
                {
                    Id = token.Id,
                    Subject = token.Subject,
                    ClientId = token.ClientId,
                    IssuedUtc = token.IssuedUtc,
                    ExpiresUtc = token.ExpiresUtc,
                    ProtectedTicket = token.ProtectedTicket
                });
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            return await _repo.ExecuteAsync("DELETE FROM dbo.RefreshToken WHERE Id = @RefreshTokenId",
                new { RefreshTokenId = refreshTokenId });
        }


        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            return await _repo.ExecuteAsync("DELETE FROM dbo.RefreshToken WHERE ClientId = @ClientID AND Subject = @Subject",
                new { Subject = refreshToken.Subject, ClientId = refreshToken.ClientId });
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var tokens = await _repo.QueryAsync<RefreshToken>("SELECT * FROM dbo.RefreshToken WHERE Id = @RefreshTokenId", new { RefreshTokenId = refreshTokenId });
            return tokens.FirstOrDefault();
        }

        public async Task<IEnumerable<RefreshToken>> GetAllRefreshTokens()
        {
            return await _repo.QueryAsync<RefreshToken>("SELECT * FROM dbo.RefreshToken");
        }
    }
}