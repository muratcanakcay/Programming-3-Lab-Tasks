#include <iostream>
#include <fstream>
#include "graph.h"   // You can't delete nor comment 
#include "graph.h"   // any of these two lines

using namespace std;
using namespace graphs;

int main()
    {
    fstream f;
    
    graph g;
    vector<pair<int, int>> edges;
    vector<int> nodes;

    // Testing add__node()
    cout << "TESTING add_node()" << endl;
    cout << "Current graph is empty" << endl << g << endl;
    cout << "Adding node -5 : " << (g.add_node(-5) ? "Success" : "Fail") << endl;
    cout << g;
    cout << "Adding node -5 again (should fail): " << (g.add_node(-5) ? "Success" : "Fail") << endl;
    cout << g;
    cout << "Adding node -3 (should succeed): " << (g.add_node(-3) ? "Success" : "Fail") << endl;
    cout << g << endl;
    
    // Testing del_node()
    cout << "TESTING del_node()" << endl;
    cout << "Current graph: " << endl << g; 
    cout << "Deleting node -5 (should succeed): " << (g.del_node(-5) ? "Success" : "Fail") << endl;
    cout << g;
    cout << "Deleting node -5 again (should fail): " << (g.del_node(-5) ? "Success" : "Fail") << endl;
    cout << g;
    cout << "Deleting node -3 (should succeed): " << (g.del_node(-3) ? "Success" : "Fail") << endl;
    cout << g;
    
    cout << endl << "Adding nodes 1, 2, 3, 4, 5" << endl;
    g.add_node(1);
    g.add_node(2);
    g.add_node(3);
    g.add_node(4);
    g.add_node(5);
    cout << "Current graph: " << endl << g << endl;

    // Testing nodes()
    cout << "TESTING nodes()" << endl; 
    nodes = g.nodes();
    cout << "The nodes of the graph are:" << endl << "[ ";
    for (const auto& n : nodes)
        cout << n << " ";
    cout << "]" << endl << endl;

    // Testing add_edge()
    cout << "TESTING add_edge()" << endl;
    cout << "Current graph: " << endl << g; 
    cout << "Adding edge (1, 2) (should succeed) : " << (g.add_edge(1, 2) ? "Success" : "Fail") << endl;
    cout << g;
    cout << "Adding edge (1, 2) again (should fail) : " << (g.add_edge(1, 2) ? "Success" : "Fail") << endl;
    cout << g;
    cout << "Adding edge (2, 4) (should succeed) : " << (g.add_edge(2, 4) ? "Success" : "Fail") << endl; 
    cout << g; 
    cout << "Adding edge (7, 2) (should fail) : " << (g.add_edge(1, 2) ? "Success" : "Fail") << endl;
    cout << g;
    cout << "Adding edge (1, 7) (should fail) : " << (g.add_edge(1, 2) ? "Success" : "Fail") << endl;
    cout << g << endl;
    
    // Testing del_edge()
    
    cout << "TESTING del_edge()" << endl;
    cout << "Deleting edge (-7, 2) (should fail) : " << (g.del_edge(-7, 2) ? "Success" : "Fail") << endl; 
    cout << g;
    cout << "Deleting edge (1, 9) (should fail) : " << (g.del_edge(1, 2) ? "Success" : "Fail") << endl;
    cout << g;
    cout << "Deleting edge (1, 2) (should succeed) : " << (g.del_edge(1, 2) ? "Success" : "Fail") << endl;
    cout << g;
    cout << "Deleting edge (1, 2) again (should fail) : " << (g.del_edge(1, 2) ? "Success" : "Fail") << endl;
    cout << g;
    cout << "Deleting edge (2, 4) (should succeed) : " << (g.del_edge(2, 4) ? "Success" : "Fail") << endl;
    cout << g << endl;
    
    // Testing out_edges()
    edges = g.out_edges(1);
    cout << "TESTING out_edges()" << endl;
    cout << "The list of edges outgoing from node 1 is (should be empty):" << endl;
    for (auto e : edges)
        cout << "( " << e.first << " -> " << e.second << " )" << endl << endl;
    
    cout << endl << "Adding edges (1, 2), (1, 3), (1, 5) (4 , 2), (4, 5), (5, 3) " << endl; 
    g.add_edge(1, 2);
    g.add_edge(1, 3);
    g.add_edge(1, 5);
    g.add_edge(4, 2);
    g.add_edge(4, 5);
    g.add_edge(5, 3);
    cout << g;
    
    edges = g.out_edges(1); 
    cout << "The list of edges outgoing from node 1 is (should be (1, 2), (1, 3), (1, 5)):" << endl;
    for (auto e : edges)
        cout << "(" << e.first << ", " << e.second << ")" << endl;

    edges = g.out_edges(4);
    cout << "The list of edges outgoing from node 4 is (should be (4, 2), (4, 5)):" << endl;
    for (auto e : edges)
        cout << "(" << e.first << ", " << e.second << ")" << endl;
    cout << endl;

    // TESTING nodes_count() and edges_count()
    cout << "TESTING nodes_count() and edges_count()" << endl;
    cout << "No. of nodes: " << g.nodes_count() << endl;
    cout << "No. of edges: " << g.edges_count() << endl;
    cout << g << endl;
    
    // Testing reading graph from file using >> operator (assuming graph is present)
    cout << "TESTING reading graph from file using >> operator" << endl;
    f.open("graph1.txt", ios::in);
    if (!f.is_open())
    {
        cout << "error in openning file graph1.txt" << endl;
        return -1;
    }
    f >> g;
    f.close();
    cout << "Graph read from file graph1.txt:" << endl << g << endl;
    
    // Testing obtaining the reverse of the graph
    cout << "TESTING graph::reverse()" << endl; 
    graph r = graph::reverse(g);
    cout << "Reverse of the graph in graph1.txt:" << endl << r << endl;

    // Testing writing to a file 
    cout << "TESTING writing the reversed graph to file output.txt using << operator" << endl;
    f.open("output.txt", ios::out);
    f << r;
    f.close();
        
    f.open("output.txt", ios::in);
    if (!f.is_open())
    {
        cout << "error in openning file graph1.txt" << endl;
        return -1;
    }
    cout << "Reversed graph written to file output.txt." << endl;
    cout << "Graph g before reading from file output.txt:" << endl << g;
    f >> g;
    f.close();
    cout << "Graph g after reading from file output.txt (should be reversed):" << endl << g << endl;
    
    
    // ------------------------------------------
    
    
    cout << endl << "shortest paths tests" << endl;
    graph g1;
    f.open("graph1.txt", ios::in);
    if ( !f.is_open() )
        {
        cout << "error in openning file graph1.txt" << endl ;
        return -1;
        }
    f >> g1;
    f.close();
    cout << g1 ;


    vector<int> v;
    v = graph::shortest_path(g1, 3, 7);
    for ( int n : v )
        cout << n << " " ;
    cout << endl ;

    cout << endl << "nodes coloring tests" << endl;
    graph g2;
    f.open("graph2.txt", ios::in);
    if ( !f.is_open() )
        {
        cout << "error in openning file graph2.txt" << endl ;
        return -1;
        }
    f >> g2;
    f.close();
    cout << g2 ;

    map<int,int> m;
    int c;
    c = graph::coloring(g2, m);
    cout << endl << c << endl ;
    for ( auto nc : m )
        cout << nc.first << " " << nc.second << endl ;


    
    return 0;
    }
