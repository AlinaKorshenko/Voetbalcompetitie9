// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace ChampionsLeagueTickets.Areas.Identity.Pages.Account
{
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ResetPasswordModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "E-mailadres is verplicht in te vullen.")]
            [EmailAddress(ErrorMessage = "E-mailadres is verplicht in te vullen.")]
            [Display(Name = "E-mail")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "Wachtwoord is verplicht in te vullen.")]
            [StringLength(100, ErrorMessage = "Het wachtwoord moet ten minste {2} en maximum {1} karakters lang zijn.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Wachtwoord")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "Wachtwoordbevestiging is verplicht in te vullen.")]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Het wachtwoord en de bevestiging zijn niet gelijk aan elkaar.")]
            [Display(Name = "Bevestig wachtwoord")]
            public string ConfirmPassword { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            public string Code { get; set; }

        }

        public IActionResult OnGet(string code = null)
        {
            if (code == null)
            {
                return BadRequest("Een code moet gegeven worden voor de wachtwoord reset.");
            }
            else
            {
                Input = new InputModel
                {
                    Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code))
                };
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
            if (result.Succeeded)
            {
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                var message = error.Code switch
                {
                    "DuplicateUserName" => "Dit e-mailadres is al in gebruik.",
                    "DuplicateEmail" => "Dit e-mailadres is al in gebruik.",
                    "PasswordTooShort" => "Wachtwoord moet minstens 6 tekens lang zijn.",
                    "PasswordRequiresNonAlphanumeric" => "Wachtwoord moet minstens één speciaal teken bevatten.",
                    "PasswordRequiresDigit" => "Wachtwoord moet minstens één cijfer bevatten.",
                    "PasswordRequiresUpper" => "Wachtwoord moet minstens één hoofdletter bevatten.",
                    "PasswordRequiresLower" => "Wachtwoord moet minstens één kleine letter bevatten.",
                    _ => error.Description
                };
                ModelState.AddModelError(string.Empty, message);
            }
            return Page();
        }
    }
}
