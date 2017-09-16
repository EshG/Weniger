using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Weniger.UiServices;
using Weniger.UiServices.Augmentors;

namespace Host.Wpf.Augmentors
{
    public class CustomerCardAugmentor : DataEntryAugmentor
    {
        Data.Customer customer;

        public CustomerCardAugmentor(int userId)
        {
            using (Data.AdventureWorksLTEntities1 context = new Data.AdventureWorksLTEntities1())
            {
                context.Configuration.LazyLoadingEnabled = false;
                customer = context.Customers.Where(c => c.CustomerID == userId).FirstOrDefault();
            }
        }

        public override async Task OnInput(IInput input, UserItem[] items)
        {
            foreach (PropertyUserItem item in items)
            {
                PropertyInfo prop = item.Property;
                if (item.Value != prop.GetValue(customer))
                    prop.SetValue(customer, item.Value);
            }


            using (Data.AdventureWorksLTEntities1 context = new Data.AdventureWorksLTEntities1())
            {
                await context.SaveChangesAsync();
            }
        }


        public async override Task OnInputValidation(IInput input, UserItem[] items)
        {
            foreach (PropertyUserItem item in items)
            {
                if (string.IsNullOrWhiteSpace(item.Value as string))
                {
                    await Task.FromResult($"{item.Property.Name} is empty.");
                }
            }

            await Task.CompletedTask;
        }


        string[] FieldsToEdit = new string[]
        {
            "FirstName","LastName","CompanyName","EmailAddress"
        };

        public override async Task<UserItem[]> OnOutput()
        {
            return await Task.FromResult(PropertyUserItem.FromModel(customer).Where(f => FieldsToEdit.Contains(f.Property.Name)).ToArray());
        }
    }
}
