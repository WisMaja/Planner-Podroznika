using Microsoft.AspNetCore.Mvc;

[ApiController] //Informuje ASP.NET, że klasa jest kontrolerem API.
[Route("api/[controller]")] //Automatycznie ustawia ścieżkę URL jako api/users (nazwa kontrolera bez "Controller").


#region Zapytania o Uzytkownika
public class UsersController : ControllerBase{
    private readonly AppDbContext _context; //prywatna zmienna

    public UsersController(AppDbContext context){
        _context = context;
    }

    [HttpGet]
    public IActionResult GetUsers(){
        var users = _context.Users?.ToList();
        return Ok(users);
    }

    [HttpPost]
    public IActionResult CreateUser(User newUser)
    {
        if (_context.Users == null)
            return Problem("Tabela Users nie istenieje");
            
        _context.Users.Add(newUser);
        _context.SaveChanges();
        return Ok(newUser);
    }

//narzie nie działa
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
       var user = _context.Users?.Find(id);

       if (user == null)
            return NotFound(new{ message = $"Uzytkownik o id{id} nie został znaleziony w bazie danych"});

            
        _context.Users!.Remove(user);
        _context.SaveChanges();
        return Ok(new {message = $"Użytkownik o id {id} został pomyślnie usunięty"});
    }

}

#endregion

