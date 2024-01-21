using training_final.Data;

namespace training_final
{
    public class MostFunctoins
    {

        public MyAppContext Context()
        {
            var context = new MyAppContext();
            context.Database.EnsureCreated();

            return context;
        }
    }
}
