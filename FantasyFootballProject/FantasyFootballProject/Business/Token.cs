using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FantasyFootballProject.DataBase;
using FantasyFootballProject.Data_Access;
using System.Text;


namespace FantasyFootballProject.Business
{
    public class Token
    {
        public static Object LogIn(string password, string username)
        {
            if (Handle_member_data.checkUser(username, password))
            {
                return generateToken(username);
            }
            return "Your username or password is wrong!!!";
        }
        public static string generateToken(string userName)
        {
            var securityKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Your Friendly Neighborhood Spiderman"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("UserName", userName),
            };
            var token = new JwtSecurityToken(
                issuer: "http://localhost:7096",
                audience: "http://localhost:7096",
                claims,
                expires: DateTime.Now.AddSeconds(1),
                signingCredentials: credentials
            );
            var stringToken = new JwtSecurityTokenHandler().WriteToken(token);
            return stringToken;

        }
        public static string decodeToken(string Token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(Token);
            var x = jsonToken.Claims.First(claim => claim.Type == "UserName").Value;
            return x;
        }
    }
}

