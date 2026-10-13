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

    [HttpPost("create")]
    public async Task<IActionResult> CreateAccountAsync([FromBody] AccountDtoCreate accountDto, CancellationToken ct = default)
    {
        var accountNumber = GenerateAccountNumber.Generate();
        
        var account = new Account(accountNumber)
        {
            AccountHolder = accountDto.AccountHolder
        };

        if(account.AccountHolder == null || account.AccountHolder.Length < 10)
        {
            return BadRequest("Account holder name must be at least 10 characters long.");
        }

        if(account.AccountHolder.Length > 100)
        {
            return BadRequest("Account holder name must not exceed 100 characters.");
        }

        if(account.AccountHolder.Any(char.IsDigit))
        {
            return BadRequest("Account holder name must not contain numbers.");
        }

        await _context.Accounts.AddAsync(account, ct);
        
        await _context.SaveChangesAsync(ct);
        
        return Ok("Account Created");
    }
}