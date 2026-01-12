using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies; // <--- Adicionado para pegar o nome padrão do esquema
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class LoginController : Controller
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] User user)
    {
        // 1. Validação inicial: O pacote chegou inteir"o? (Campos preenchidos, etc)
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // 2. A "Portaria": Verifica se a senha e o email estão corretos.
        // MUDANÇA: Fazemos isso ANTES de criar qualquer coisa.
        if (user.Password != "senha123" || user.Email != "email@example.com")
        {
            return Unauthorized(new { Message = "Credenciais inválidas." });
        }

        // ==================================================================
        // 3. Área de Criação do Crachá (Só roda se a senha estiver certa!)
        // ==================================================================

        var claims = new List<Claim>
        {
            // O Nome do usuário (útil para mostrar "Olá, [Email]" na tela)
            new Claim(ClaimTypes.Name, user.Email),

            // SEGURANÇA: Trocamos a senha por um ID fictício "1". 
            // Nunca coloque a senha aqui!
            new Claim(ClaimTypes.NameIdentifier, "1") 
            
            // Dica: Se quiser definir permissões, adicionaria: new Claim(ClaimTypes.Role, "Admin")
        };
        
        // Aqui definimos que esse crachá pertence ao sistema de Cookies
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        
        // 4. Entrega o crachá (Cria o Cookie no navegador)
        // Nota: Usei "CookieAuthenticationDefaults.AuthenticationScheme" para garantir que bata com o Program.cs
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        return Ok(new { Message = "Login bem-sucedido!" });
    }
}