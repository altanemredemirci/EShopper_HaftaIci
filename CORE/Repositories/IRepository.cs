using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Repositories
{
    //Async: Kod akışında bir asenkron metot çalıştığında, kod akışı bu metodun sonucu beklemeden çalışmaya devam eder. Arkaplanda tamamlanan metot işlemi geri gelerek yapması gerek işi tamamlar.
    //Bu sayede aynı anda birden fazla işlem tamamlanmış olur.
    //Asenkron yerine senkron tercih edilmesi kod akışında metodun geri gelmesini beklemek anlamına gelir.

    //Asenkron metotlar bize daha performanslı işlemler yapmanın yolunu açar.

    //BLL ve DAL içinde kullanılacak ana metotları tanımlamak

    public interface IRepository<T> where T : class
    {
        //CQRS: okuma ve manipülasyon metotlarının ayrı çalıştırılması. Çünkü okuma metotları data manipüle etmekdikleri için daha performanslıdır. Öyleyse bu iki tip metodu aynı kanaldan çalıştırmak performans kaybına sebep olur.
        Task CreateAsync(T entity);
        Task UpdateAsync();
        Task DeleteAsync(int id);

        //IReadOnlyList: çekilen dataların sadece okunabilir olduğu tanımlar. Bu sayede AsTracking():takip etme devreye girer ve gelen datanın yolu unutulur. 
        Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
        Task<T> GetOneAsync(Expression<Func<T, bool>> filter);
        Task<T> FindAsync(int id);
    }
}
