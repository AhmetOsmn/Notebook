namespace Company.Activities
{
    using System.Threading.Tasks;
    using MassTransit;

    public class TemplatesActivity :
        IExecuteActivity<TemplatesArguments>
    {
        public async Task<ExecutionResult> Execute(ExecuteContext<TemplatesArguments> context)
        {
            await Task.Delay(100);

            return context.Completed();
        }
    }
}