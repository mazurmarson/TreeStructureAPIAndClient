using System.Collections.Generic;
using System.Linq;
using TreeAPI.Helpers;
using TreeAPI.Models;

namespace TreeAPI
{
    public static class ExtensionMethod
    {
        public static IList<NodeWithChilds> BuilldTree(this IEnumerable<NodeWithChilds> source)
        {
            var nodes = source.GroupBy(x => x.ParentId);

            var roots = nodes.FirstOrDefault(x => x.Key.HasValue ==false).ToList();

            if(roots.Count > 0)
            {
                var dict = nodes.Where(x => x.Key.HasValue).ToDictionary(x => x.Key.Value, g => g.ToList());
                    for(int i = 0; i < roots.Count; i++)
                        AddChildren(roots[i], dict);
            }

            return roots;
        }

        private static void AddChildren(NodeWithChilds node, IDictionary<int, List<NodeWithChilds>> source)
        {
            if(source.ContainsKey(node.Id))
            {
                node.Childs = source[node.Id];
                    for(int i = 0; i < node.Childs.Count; i++)
                        AddChildren(node.Childs[i], source);
            }
            else
            {
                node.Childs = new List<NodeWithChilds>();
            }
        }
    }
}