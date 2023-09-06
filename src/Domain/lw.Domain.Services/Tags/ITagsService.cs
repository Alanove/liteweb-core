namespace lw.Domain.Services;

public interface ITagsService
{
	Tag? GetTag(string tagName);
	Tag AddTag(Tag tag);
	List<Tag> AddTags(List<Tag> tags);

    IEnumerable<Tag> GetTags();
	void DeleteTag(Tag tag);
}
