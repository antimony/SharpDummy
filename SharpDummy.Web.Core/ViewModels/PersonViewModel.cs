using AutoMapper;
using SharpDummy.Infrastructure.Domain.Entities;

namespace SharpDummy.Web.Core.ViewModels
{
	public class PersonViewModel
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }

		public PersonViewModel(){}

		public PersonViewModel(Person person)
		{
			Mapper.Map(person, this);
		}
	}
}
