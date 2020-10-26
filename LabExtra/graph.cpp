#ifndef GRAPH_CPP
#define GRAPH_CPP

#include "graph.h"

using namespace std;

namespace graphs
{
	bool graph::add_node(int n)
	{
		unordered_set<int> neighbors; 
		bool result = edges.insert(make_pair(n, neighbors)).second; // returns false on failure
				
		return result;
	}
	bool graph::del_node(int n)
	{
		bool result;
		
		if (edges.find(n) == edges.end()) // node n does not exist
			result = false;
		else							  // node n exists
		{
			ecount -= edges[n].size(); // reduce total edge count by number of edges outgoing from node n
			edges.erase(n);

			for (auto& e : edges)
				ecount -= e.second.erase(n); // remove edges ingoing to node n and reduce total edge count 

			result = true;
		}
				
		return result;
	}
	bool graph::add_edge(int n1, int n2)
	{
		bool result;

		try
		{
			auto checkn2 = edges.at(n2); // check n2 exists
			result = edges.at(n1).insert(n2).second; // check n1 exists and try to add edge n1->n2
		}
		catch (const out_of_range& e) // n1 or n2 does not exist
		{			
			result = false;
		}
		
		if (result) ecount++; 
		return result;
	}
	bool graph::del_edge(int n1, int n2)
	{
		bool result;

		try
		{
			result = (edges.at(n1).erase(n2) == 1); // check n1 exists and try to remove edge n1->n2
		}
		catch (const out_of_range& e) // n1 does not exist
		{		
			result = false;
		}
		
		if (result) ecount--;
		return result;
	}
	vector<int> graph::nodes()
	{
		vector<int> nodes;

		for (const auto& e : edges)
			nodes.push_back(e.first);
		
		return nodes;
	}
	vector<pair<int, int>> graph::out_edges(int n)
	{
		vector<pair<int, int>> edge_list;

		for (auto e : edges[n])
			edge_list.push_back(make_pair(n, e));
		
		return edge_list;
	}
	ostream& operator<<(ostream& out, const graph& g)
	{
		for (auto e : g.edges) 
		{
			out << e.first << " : "; // node
			
			for (auto n : e.second) // neighbors
				out << n << " ";
			out << ";" << '\n';
		}
				
		return out;
	}
	istream& operator>>(istream& in, graph& g)
	{
		// clear g for rewriting
		g.edges.clear();
		g.ecount = 0;
		
		// read from stream and update graph
		char buffer[4096];
		int node;
		unordered_set<int> neighbors;

		while (in.peek() != EOF) // read line by line until EOF
		{
			in.getline(buffer, sizeof(buffer), ':'); // read the integer before ":" 
			node = stoi(buffer); 
g.edges.insert(make_pair(node, neighbors)); // enter node into map

in.get(); // jump over the space after ":" 

while (in.peek() != ';') // read the integers after ":" one by one until ";" 
{
	in.getline(buffer, sizeof(buffer), ' ');
	if (g.edges[node].insert(stoi(buffer)).second) g.ecount++; // enter neighbors into map and increase ecount
}

in.get(); // jump over ";"
in.get(); // jump over end of line character
		}

		return in;
	}
	graph graph::reverse(const graph& g)
	{
		graph r;

		// copy nodes
		for (auto e : g.edges)
			r.add_node(e.first);

		// enter reversed edges
		for (auto e : g.edges)
			for (auto n : e.second)
				r.add_edge(n, e.first);

		return r;
	}
	vector<int> graph::shortest_path(graph& g, int n1, int n2)
	{
		vector<int> path;
		map<int, int> parents = analyze(g, n1); // initialize the map of nodes & parents

		if (parents.count(n2) == 0) return path; // if n2 is not in the map then no path between n1 & n2

		//trace back from n2 to n1 and store the path
		for (int node = n2; node != n1; node = parents[node])
			path.push_back(node);
		path.push_back(n1);

		std::reverse(path.begin(), path.end());

		return path;
	}
	map<int, int> graph::analyze(graph& g, int n1) // bfs helper function for finding shortest path 
	{
		// data structures
		queue<int> q;
		map<int, bool> visited;
		map<int, int> parents;

		//initialize list of visited nodes
		for (auto e : g.edges)
			visited[e.first] = false;
		visited[n1] = true;

		//initialize queue
		q.push(n1);

		// bfs algorithm
		while (!q.empty())
		{
			for (auto n : g.edges[q.front()])
			{
				if (!visited[n])
				{
					q.push(n);
					visited[n] = true;
					parents[n] = q.front();
				}
			}

			q.pop();
		}

		// returns a map of nodes & parents
		return parents;
	}
		
	int graph::coloring(graph& g, map<int, int>& colors)
	{
		// initialize variables
		int n1 = g.edges.begin()->first; // first node
		int best_colors_number = g.nodes_count() + 1; 
		int act_color_number = 0;
		map<int, int> act_colors;

		//first node gets first color
		act_colors[n1] = 1;
		act_color_number++;

		int n2 = (++g.edges.find(n1))->first; // second node
		
		// go into recursion with second node
		coloring_rec(g, n2, best_colors_number, colors, act_color_number, act_colors);
		
		return best_colors_number;
	}
	void graph::coloring_rec(graph& g, int n, int& best_colors_number, map<int, int>& best_colors, int _act_color_number, map<int, int>& _act_colors)
	{
		// we can use as many colors as the nodes 
		for (int color = 1; color < g.nodes_count() + 1; color++)
		{
			// reset actual color numbers and color map to initial values at each iteration
			map<int, int> act_colors = _act_colors;
			int act_color_number = _act_color_number;
			
			// check if color is available, i.e. no neighbors with same color
			if (isAvailable(g, color, n, act_colors))
			{
				//check if color was used before, if not increase act_color_number
				bool color_used = false;
				for (auto c : act_colors)
					if (c.second == color) 
					{
						color_used = true;  
						break;
					}
				if (!color_used) act_color_number++; 
				
				act_colors[n] = color; // update actual color map

				if ((++g.edges.find(n)) != (g.edges.end())) // if not the last node continue recursion with next node
					coloring_rec(g, (++g.edges.find(n))->first, best_colors_number, best_colors, act_color_number, act_colors);
				else // if last node
				{
					// check if better than best result and if yes, update best result 
					if (act_color_number < best_colors_number) 
					{						
						best_colors = act_colors;
						best_colors_number = act_color_number;
					}
				}
			}
			// continue with next color
			// if all colors used backtrack to previous node 
		}
		return; 
	}
	bool graph::isAvailable(graph& g, int color, int n, map<int, int>& act_colors)
	{
		// helper function to determine if a color can be used on a node
		
		// check if the color was used in any neighbor at outgoing edges
		for (auto node : g.edges[n]) 
			if (act_colors[node] == color)
				return false;
		
		// check if the color was used in any neighbor at incoming edges
		for (auto node : g.edges)
			for (auto neighbor : node.second)
				if (neighbor == n && act_colors[node.first] == color)
					return false;

		// if color was not used in any neighbor it is available for use
		return true;
	}
}


#endif