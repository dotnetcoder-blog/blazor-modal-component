using Dnc.Common.Razor.Modal;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace Dnc.Modal.WebApp.Components.Pages
{
    public class HomeComponent: ComponentBase
    {
        protected DncModal<Account> EditModal { get; set; }

        protected DncModal<Account> AddModal { get; set; }

        // Edit Modal Methods
        protected void EditModalSubmitted(Account account)
        {
            var exist = Accounts.FirstOrDefault(v => v.Id == account.Id);

            if (exist != null && EditModal.EditContext.Validate())
            {
                // Update the account in real projects
                EditModal.Close();
            }

        }

        protected void EditModalDisplayed(Account account)
        {
            EditModal.SetEditContext(new EditContext(account));
        }

        // Add Account Modal Methods
        protected Account AccountModel { get; set; } = new Account()
        {
            Id = Guid.NewGuid().ToString(),
            ExpireDate = DateTime.Now.AddYears(1)
        };

        protected void AddModalSubmitted(Account account)
        {
            var exist = Accounts.FirstOrDefault(v => v.Id == account.Id);

            if (exist == null && AddModal.EditContext.Validate())
            {
                Accounts.Add(account);
                AddModal.Close();
            };
        }

        protected void AddModalDisplayed(Account account)
        {
            AddModal.SetEditContext(new EditContext(account));
        }

        protected List<Account> Accounts = new List<Account>
    {
        new Account {Id = Guid.NewGuid().ToString() , Name = "John Smith", Email= "john.smith@coder.com",  Age = 20, ExpireDate = DateTime.Now.AddYears(1)},
        new Account {Id = Guid.NewGuid().ToString() , Name = "Sarah Johnson", Email= "Sarah.Johnson@coder.com", Age = 30, ExpireDate = DateTime.Now.AddYears(1)},
        new Account {Id = Guid.NewGuid().ToString() , Name = "Michael Brown", Email= "Michael.Brown@coder.com", Age = 40, ExpireDate = DateTime.Now.AddYears(1)},
        new Account {Id = Guid.NewGuid().ToString() , Name = "Emily Davis", Email= "Emily.Davis@coder.com", Age = 50, ExpireDate = DateTime.Now.AddYears(1)}
    };
    }

    public class Account
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Insert valid email address")]
        public string Email { get; set; }
        [Range(18, 80, ErrorMessage = "The Age should be between 18 and 65 years")]
        public int Age { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
