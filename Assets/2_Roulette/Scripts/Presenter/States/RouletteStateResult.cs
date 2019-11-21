namespace PM.Roulette
{
    public partial class RoulettePresenter
    {
        public class RouletteStateResult: RouletteState
        {
            public RouletteStateResult(RoulettePresenter presenter):base(presenter)
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