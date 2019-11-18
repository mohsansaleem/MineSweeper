namespace PM.Roulette
{
    public partial class RoulettePresenter
    {
        public class RouletteStateGamePlay: RouletteState
        {
            public RouletteStateGamePlay(RoulettePresenter presenter):base(presenter)
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