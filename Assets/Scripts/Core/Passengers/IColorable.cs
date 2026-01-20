using Core.Data;

namespace Core.Passengers {
	public interface IColorable {
		public ColorDefinition GetColorDefinition();
		public void SetColorDefinition(ColorDefinition colorDefinition);
	}
}