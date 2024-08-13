using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Extension
{
    public class OperationVisitor : ExpressionVisitor
    {
        private ParameterExpression _parameter;
        public OperationVisitor(ParameterExpression parameter)
        {
            this._parameter = parameter;
        }
        public Expression Modify(Expression expression)
        {
            return this.Visit(expression);
        }
        [return: NotNullIfNotNull("node")]
        public override Expression? Visit(Expression? node)
        {
            var res = base.Visit(node);
            return res;
        }
        /// <summary>
        /// 当表达式是二元的时候触发调用
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitBinary(BinaryExpression node)
        {
            //遇到add进行拆分
            if (node.NodeType == ExpressionType.Add)
            {
                //解析成左右2个节点；然后对2个节点再次解析
                Expression left = this.Visit(node.Left);
                Expression right = this.Visit(node.Right);
                //重新组装成Subtract
                return Expression.Subtract(left, right);
            }
            //返回出二元表达式
            return base.VisitBinary(node);
        }
        //对字段属性进行处理
        protected override Expression VisitMember(MemberExpression node)
        {
            return base.VisitMember(node);
        }

        public Expression Replace(Expression body)
        {
            return this.Visit(body);
        }
        protected override Expression VisitParameter(ParameterExpression node)
        {
            return _parameter;
        }
    }
}
