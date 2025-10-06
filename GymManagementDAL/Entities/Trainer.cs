using GymManagementDAL.Entities.Enums;

namespace GymManagementDAL.Entities;

public class Trainer : GymUser
{
    public Specialties Specialties { get; set; }
    public ICollection<Session> TrainerSessions { get; set; } = null!;
}
