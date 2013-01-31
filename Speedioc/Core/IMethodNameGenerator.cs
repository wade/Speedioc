namespace Speedioc.Core
{
	public interface IMethodNameGenerator
	{
		string CoreMethodName { get; set; }
		string GenerateCreateInstanceMethodName();
		string GenerateCreateThreadLocalInstanceMethodName();
		string GenerateOperationMethodName();
	}
}