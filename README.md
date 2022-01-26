# FlowFields
Research topic
# Intro

A flow field is simply said a sample space that returns a velocity at every position. We will focus on a 2D grid where every field will return a veloctiy.
These velocities will depend on attraction or repulsion from certain entities.

Flow fields are a powerful pathfinding/steering behaviour technique in some cases. 
A few of these cases are,
- you have a large amount of agents going to the same position.
- the environment is highly dynamic
- the position of the agents need to be constantly changed

A flow field consists of 3 fields.

# 1. Cost Field
In the cost field every cell will be given a value normally between 0 and 255. At the start every cell will be given 1 and the goal 0.
255 is normally used for impassable terrain and everything between 2 and 254 is used to indicate terrain that you would like to avoid.
The higher the cost the more need to avoid it.

# 2. Integration Field
The integration field starts off by giving every cell a high value, I use int maxValue. The goalNode will be set to 0 and added to a list.
Loop over the list untill the size is zero, for every element check all the neighbours. If the neighbours cost value + the integration value of current cell is smaller than the
neighbours integration value set the integration value of the neigbour to his cost value + current cell integration value and add it to the list.

# 3. Flow Field (also called vector field)
The flow field loops over all the cells and checks for every cell which neighbour of his has the smallest integration value. The flowfield value of that cell gets set to the ID of the neighbour - the ID of the current cell which results in a vector.

# Program Info
The info box on the right side will show you which mode you are in.
B will turn on Blue mode, K will turn on RED mode
Press the same button again of the mode you are in to return to Green mode

Green: 

LMB: Set a new goal point

RMB: Erase impassable and rough paths created by blue mode

BLUE:

LMB: Set cell as impassable terrain

RMB: Set cell as rough terrain with cost value 4

RED:

LMB: Place cube in gameworld at cell position (automatically sets the cost value to 255)

RMB: Deletes placed cube

Showing info:

1: Show every cells cost value (press again when you want a refreshed view)

2: Show every cells integration value (press again when you want a refreshed view)

3: Show the direction of every cell to where the velocity of the flowfield value (velocity) pushes  
