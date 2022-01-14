var pass = "adpasdp1";
var hashPass = BCrypt.Net.BCrypt.HashPassword(pass);

var enterPass = "sdgdgsdgdg";
var enterHashPass = BCrypt.Net.BCrypt.Verify(enterPass, hashPass);

Console.WriteLine($"{pass} {hashPass}");

Console.WriteLine($"{enterPass} {enterHashPass}");
