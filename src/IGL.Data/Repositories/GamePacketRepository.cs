﻿
namespace IGL.Data.Repositories
{
    public class GamePacketRepository : BaseTableRepository<ServiceEntities.GamePacketEntity>
    {    
        // partition is the session of the game        
        // rowkey is a number generated by the client unique within the session of the game
        
        public GamePacketRepository(int gameId) : base(string.Format("GamePacket{0:0000000000}", gameId), 101)
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<GamePacket, ServiceEntities.GamePacketEntity>()
                .ForMember(m => m.PartitionKey, s => s.MapFrom(g => g.Correlation))
                .ForMember(m => m.RowKey, s => s.MapFrom(g => g.PacketNumber));

                cfg.CreateMap<ServiceEntities.GamePacketEntity, GamePacket>();
            });
        }

        public AzureResult AddGamePacket(GamePacket packet)
        {
            return InsertOrReplace(packet);
        }
    }
}
