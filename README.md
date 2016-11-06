# driver-challange

### Getting Started
1. Open GenomeMerger.sln
2. Run the tests at GenomeMergerTest
2. Compile GenomeMergerApp
3. Find the exe in the bin dir and using a cmd run: 
```
GenomeMergerApp.exe <genome-part-file>.txt
```

### Merging Genome Parts
The problem of merging a set Genome parts into one string which contains every part as a substring, and where for each two genome parts, if they overlap by at least half of their characters then they should overlap in the result string can be reduced to the problem of finding a hemiltonian path within a directed graph.
The graph is built in the following way: 

for each two parts, partA connects to partB iff the two parts overlap by at least half of their characters.

__If there is a hemiltonian path in the graph described then that path is a valid ordering of the genome parts.__

Intuitive proof. Take a hemiltonian path within a graph built in manner above, examine two arbitrary nodes where one precedes the other in the path. The nodes are connected therefore they overlap by at least half of their length which follows that this is a valid ordering of the two parts in a result of the Merging parts problem. Since the path covers every node in the graph and each node (corresponding to a part) appears only once, then it is a valid ordering of the genome parts alltogether.

On the other way, taking a result of the Merge parts problem, before removing overlaps, and examining two arbitrary parts, one precedes the other iff half of their characters overlap. According to the way the graph was built there will be an edge from the preceding to the following node, representing these parts in the graph. Since in the result all genome parts appear and each part appears only once, then the corresponding path is a hemiltonian path within the graph built.

### Implementation Details
There are 3 projects in the solution:
* GenomeMerger - A library exposing the API.
* GenomeMergerApp - A simple console application utilizing the Merger lib.
* GenomeMergerTest - Unit tests.

The Main flow appears in GenomeMerger/GenomeMerger.cs under the the _Merge_ function and is as follows:
1. Construct a directed graph using the relation between genome parts as described above.
2. Find a hemiltonian path starting at any of the nodes.
3. If a path exist, merge the parts sequentially where they overlap.

##### Assumptions and outcomes 
1. Parts are approx 1000 char long, and are rather arbitrary, hence, the nunmber of relations and, following that, edges in the graph, is expected to be very low but at least single exit degree per node, since there is a unique path. With that in mind the most trivial implementation for finding hemiltonian path was used.
2. Since words are very long and their alphabet is limited, only 4 chars, a more sophisticated implementation was used in order to find how words overlap. This reduces the runtime for each two word comparisement to O(wordA + wordB) instead of the trivial O(wordA * wordB) solution.
