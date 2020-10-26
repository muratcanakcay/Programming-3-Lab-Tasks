#ifndef GRAPH_H
#define GRAPH_H

#include <iostream>
#include <vector>
#include <map>
#include <unordered_map>
#include <unordered_set>
#include <string>
#include <algorithm>
#include <queue>

using namespace std;

namespace graphs
{
class graph
    {
    public:
    bool add_node(int n);
    bool del_node(int n);
    bool add_edge(int n1, int n2);
    bool del_edge(int n1, int n2);
    vector<int> nodes();
    vector<pair<int,int>> out_edges(int n);
    int nodes_count() const { return edges.size(); }; // constant time
    int edges_count() const { return ecount; }; // constant time

    friend ostream& operator<<(ostream &out, const graph& g);
    friend istream& operator>>(istream &in, graph& g);

    static graph reverse(const graph& g);
    static vector<int> shortest_path(graph& g, int n1, int n2);
    static int coloring(graph& g, map<int,int>& colors);

    //helper methods:
    static map<int, int> analyze(graph& g, int n1);
    static bool isAvailable(graph& g, int color, int n, map<int, int>& act_colors);
    
    private:
    unordered_map<int,unordered_set<int>> edges;
    int ecount = 0;
    static void coloring_rec(graph& g, int n, int& best_colors_number, map<int,int>& best_colors, int _act_color_number, map<int,int>& _act_colors);
    };
}

#endif
