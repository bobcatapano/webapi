﻿namespace webapi3.ViewModels
{
    public class RegisterVM
    {
        public string? Name { get; set; }
        public string? AccountTypes { get; set; }
        public string? PhoneNo { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ShopName { get; set; }
        public string? BusinessType { get; set;}
        public string? UserRole { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
