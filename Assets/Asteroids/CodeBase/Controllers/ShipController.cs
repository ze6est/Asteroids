using Asteroids.CodeBase.Input;
using Asteroids.CodeBase.Ships;

namespace Asteroids.CodeBase.Controllers
{
    public class ShipController
    {
        public ShipController(ShipInput shipInput, ShipMover shipMover, ShipRotator shipRotator, ShipShooter shipShooter)
        {
            shipInput.Moved += shipMover.OnMoved;
            shipInput.Rotated += shipRotator.OnRotated;
            shipInput.BulletShooted += shipShooter.OnBulletShooted;
            shipInput.LaserShooted += shipShooter.OnLaserShooted;
        }
    }
}
