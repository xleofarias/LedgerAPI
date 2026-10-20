using Microsoft.AspNetCore.Mvc;
using LedgerAPI.API;
using LedgerAPI.API.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

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
        
        Account account;
        try
        {
            account = new Account(accountDto.AccountNumber)
            {   
                AccountHolder = accountDto.AccountHolder
            };
        } catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }

        try
        {
            await _context.Accounts.AddAsync(account, ct);
                
            await _context.SaveChangesAsync(ct);
        } catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
        {
            return Conflict(new { message = "An account with the same account number already exists." });
        }
        
        return Created();
    }
}
