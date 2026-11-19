using Microsoft.AspNetCore.Identity;

var hasher = new PasswordHasher<object>();

var hash = hasher.HashPassword(null, "demo");

Console.WriteLine(hash);
