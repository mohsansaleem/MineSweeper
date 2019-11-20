namespace PM.Roulette
{
    public partial class RoulettePresenter
    {
        public class RouletteStateSpinner: RouletteState
        {
            public RouletteStateSpinner(RoulettePresenter presenter):base(presenter)
            {
            }

            public override void OnStateEnter()
            {
                base.OnStateEnter();
                
                View.Show();
                
                
            }
        }
    }
}