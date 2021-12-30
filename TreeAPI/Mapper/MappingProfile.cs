using AutoMapper;
using TreeAPI.Helpers;
using TreeAPI.Models;

namespace TreeAPI.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Node, NodeWithChilds>();
            CreateMap<NodeWithChilds, Node>();
        }
    }
}