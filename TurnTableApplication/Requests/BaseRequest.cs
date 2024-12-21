using System.Text;

namespace TurnTableApplication.Requests
{
    public class BaseRequest
    {
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var prop in this.GetType().GetProperties())
            {
                sb.Append(prop.Name);
                sb.Append(": ");
                sb.Append(prop.GetValue(this, null));
                sb.Append(", ");
            }

            sb.Remove(sb.Length - 2,2);

            return sb.ToString();
        }
    }
}
