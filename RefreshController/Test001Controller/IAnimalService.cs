using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DependencyInjectionAttribute;
namespace Test001Controller
{
    public interface IAnimalService
    {
        string Speak();
    }

    //[CusService(typeof(IAnimalService))]
    public class Dog : IAnimalService
    {
        public string Speak()
        {
            return "Woof!";
        }
    }
}
