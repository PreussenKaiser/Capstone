using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Scheduler.Domain.Specifications;

public sealed class SpecificationVisitor : ExpressionVisitor
{
	private readonly Expression oldExpr;
	private readonly Expression newExpr;

	public SpecificationVisitor(
		Expression oldExpr,
		Expression newExpr)
	{
		this.oldExpr = oldExpr;
		this.newExpr = newExpr;
	}

	[return: NotNullIfNotNull("node")]
	public override Expression? Visit(Expression? node)
	{
		return node == this.oldExpr
			? this.newExpr
			: base.Visit(node);
	}
}
