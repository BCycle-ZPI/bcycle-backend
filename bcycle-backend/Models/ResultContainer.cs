namespace bcycle_backend.Models
{
    public class ResultContainer<T> {
        public T Result { get; set; }

        public ResultContainer(T result)
        {
            this.Result = result;
        }
    }
}
