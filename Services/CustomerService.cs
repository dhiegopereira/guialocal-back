using Microsoft.EntityFrameworkCore;
using guialocal.Data;
using guialocal.Models;

namespace guialocal.Services
{
    public class CustomerService
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Customer Create(Customer customer) 
        {
            var customerExist = _context.Customers.FirstOrDefault(c => c.Title == customer.Title);

            if (customerExist != null) {
                throw new Exception($"Já existe um cliente cadastrado com esse nome {customer.Title}");
            }

            _context.Customers.Add(customer);

            return customer;
        }

        public Customer[] ReadAllByFilter(string title)
        {
            // Retorna uma lista de clientes filtrados pelo título
            return _context.Customers.Where(c => EF.Functions.Like(c.Title, $"%{title}%") && c.Active).ToArray();
        }

        public Customer ReadOne(string email)
        {
            // Retorna um cliente pelo email
            var customer = _context.Customers.FirstOrDefault(c => c.Email == email) ?? throw new Exception("Customer not found");
            return customer;
        }

        public Customer Update(string email, Customer customerData)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Email == email) ?? throw new Exception("Customer not found");
            customer.Title = customerData.Title;
            customer.Email = customerData.Email;

            _context.SaveChanges(); 

            return customer;
        }

        public void Delete(string email)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Email == email) ?? throw new Exception("Customer not found");
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }
    }
}
