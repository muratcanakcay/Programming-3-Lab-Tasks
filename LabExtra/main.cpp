
#include <iostream>
#include <fstream>
#include "graph.h"   // You can't delete nor comment 
#include "graph.h"   // any of these two lines

using namespace std;
using namespace graphs;

int main()
    {
    fstream f;

    // add part of code to test
    // graph class instance methods

    // uncomment next part
/*
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
*/

    return 0;
    }
