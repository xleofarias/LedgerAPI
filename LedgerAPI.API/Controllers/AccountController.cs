using Microsoft.AspNetCore.Mvc;
using LedgerAPI.API;
using LedgerAPI.API.DTOs;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly AppDbContext _context;

    public AccountController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccountAsync([FromBody] AccountDtoCreate accountDto, CancellationToken ct = default)
    {
        var accountNumber = "123ABC-DEF456";
        
        var account = new Account(accountNumber)
        {
            AccountHolder = accountDto.AccountHolder
        };

        await _context.Accounts.AddAsync(account, ct);
        
        await _context.SaveChangesAsync(ct);
        
        return Ok("Account Created");
    }
}