namespace Hotel.Business.Mappers
{
	public class CommentMapper:Profile
	{
		public CommentMapper()
		{
			CreateMap<Comment,CommentDto>().ReverseMap();
			CreateMap<Comment,CreateCommentDto>().ReverseMap();
			CreateMap<Comment,UpdateCommentDto>().ReverseMap();
			
		}
	}
}
