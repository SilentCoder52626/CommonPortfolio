﻿namespace CommonPortfolio.Domain.Models.User
{
    public class  UserReponseModel
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string UserName { get; set; }
    }
}