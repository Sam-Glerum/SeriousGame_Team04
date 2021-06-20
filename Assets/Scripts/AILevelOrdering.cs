using System.Collections;
using System.Collections.Generic;
using _Unity = UnityEngine;
using System;
using System.Linq;
using System.Diagnostics;

class SolverFactory
{
    public Solver makeSolver(SolverMethod solverMethod)
    {
        RootNodeFactory rootNodeFactory = new NodeFactory();

        switch (solverMethod)
        {
            case SolverMethod.BFS:
                return new BFSSolver(rootNodeFactory, new ClosestFactoryImpl());
            case SolverMethod.DFS:
                return new DFSSolver(rootNodeFactory);
            case SolverMethod.A_STAR:
                return new AStarSolver(rootNodeFactory, new HeuristicFactoryImpl(), new ClosestFactoryImpl());
            default:
                throw new Exception("Unknown method");
        }
    }
}

class DFSSolver : Solver
{
    private RootNodeFactory nodeFactory;

    public DFSSolver(RootNodeFactory nodeFactory)
    {
        this.nodeFactory = nodeFactory;
    }

    public List<LevelModule> solve(double prefferedDuration, List<LevelModuleData> modules)
    {
        BaseNode rootNode = nodeFactory.makeRootNode();
        return solve(prefferedDuration, modules, rootNode)?.MapToLevel();
    }

    private BaseNode solve(double prefferedDuration, List<LevelModuleData> modules, BaseNode node)
    {
        if (modules.Count == 0)
        {
            if (node.totalDuration == prefferedDuration)
                return node;
            return null;
        }

        node.GenerateChildren(modules[0]);
        BaseNode result = null;
        int index = 0;

        while (result == null && index < node.children.Count)
        {
            result = solve(prefferedDuration, modules.Skip(1).ToList(), node.children[index]);
            index++;
        }

        return result;
    }
}

class BFSSolver : Solver
{
    private RootNodeFactory nodeFactory;
    private ClosestFactory closestFactory;


    public BFSSolver(RootNodeFactory nodeFactory, ClosestFactory closestFactory)
    {
        this.nodeFactory = nodeFactory;
        this.closestFactory = closestFactory;
    }

    public List<LevelModule> solve(double prefferedDuration, List<LevelModuleData> modules)
    {
        BaseNode rootNode = nodeFactory.makeRootNode();
        IComparer<BaseNode> closest = closestFactory.makeComparer(prefferedDuration);

        Stack<BaseNode> visitedNodes = new Stack<BaseNode>();
        Queue<BaseNode> nodesToProcess = new Queue<BaseNode>();

        nodesToProcess.Enqueue(rootNode);

        BaseNode bestNode = null;

        while (nodesToProcess.Count > 0)
        {
            // Get head node (and remove)
            BaseNode node = nodesToProcess.Dequeue();

            // When all modules are used
            if (node.depth == modules.Count)
            {
                // If goal has been reached, return current node
                if (node.totalDuration == prefferedDuration)
                {
                    bestNode = node;
                    break;
                }

                // If this is the best solution so far
                if (bestNode == null || closest.Compare(bestNode, node) < 0)
                {
                    bestNode = node;
                }
            }

            // Don't visit node when already visited
            if (!visitedNodes.Contains(node))
            {
                // Generate children
                try { node.GenerateChildren(modules[node.depth]); }
                catch (ArgumentOutOfRangeException) { }

                // Add all children to nodesToProcess, so they will be visited
                node.children.ForEach((child) => nodesToProcess.Enqueue(child));

                // Mark node as visited
                visitedNodes.Push(node);
            }
        }

        return bestNode?.MapToLevel();
    }
}

class AStarSolver : Solver
{
    private RootNodeFactory nodeFactory;
    private HeuristicFactory heuristicFactory;
    private ClosestFactory closestFactory;

    public AStarSolver(RootNodeFactory nodeFactory, HeuristicFactory heuristicFactory, ClosestFactory closestFactory)
    {
        this.nodeFactory = nodeFactory;
        this.heuristicFactory = heuristicFactory;
        this.closestFactory = closestFactory;
    }

    public List<LevelModule> solve(double prefferedDuration, List<LevelModuleData> modules)
    {
        BaseNode rootNode = nodeFactory.makeRootNode();
        IComparer<BaseNode> closest = closestFactory.makeComparer(prefferedDuration);
        IComparer<BaseNode> heuristic = heuristicFactory.makeHeuristic(prefferedDuration);
        Random rnd = new Random();

        Stack<BaseNode> visitedNodes = new Stack<BaseNode>();
        PriorityQueue<BaseNode> nodesToProcess = new PriorityQueue<BaseNode>(heuristic);

        nodesToProcess.Push(rootNode);

        BaseNode bestNode = null;
        int counter = 0;

        while (nodesToProcess.Count > 0)
        {
            BaseNode node;

            // Get rendom item 10% of the time
            if (counter == 10)
            {
                node = nodesToProcess.RemoveAt(rnd.Next(nodesToProcess.Count));
                counter = 0;
            }
            else
            {
                // Get head node (and remove)
                node = nodesToProcess.Pop();
                counter++;
            }

            // When all modules are used
            if (node.depth == modules.Count)
            {
                // If goal has been reached, return current node
                if (node.totalDuration == prefferedDuration)
                {
                    bestNode = node;
                    break;
                }

                // If this is the best solution so far
                if (bestNode == null || closest.Compare(bestNode, node) < 0)
                {
                    bestNode = node;
                }
            }

            // Don't visit node when already visited
            if (!visitedNodes.Contains(node))
            {
                // Generate children
                try { node.GenerateChildren(modules[node.depth]); }
                catch (ArgumentOutOfRangeException) { }

                // Add all children to nodesToProcess, so they will be visited
                node.children.ForEach((child) => nodesToProcess.Push(child));

                // Mark node as visited
                visitedNodes.Push(node);
            }
        }

        return bestNode?.MapToLevel();
    }
}

class HeuristicFactoryImpl : HeuristicFactory
{
    public IComparer<BaseNode> makeHeuristic(double goal)
    {
        return new AStarHeuistic(goal);
    }
}

class AStarHeuistic : IComparer<BaseNode>
{
    double prefferedDuration;

    public AStarHeuistic(double prefferedDuration)
    {
        this.prefferedDuration = prefferedDuration;
    }

    private double diff(BaseNode x)
    {
        return Math.Abs(prefferedDuration - x.totalDuration);
    }

    public int Compare(BaseNode x, BaseNode y)
    {
        double d1 = diff(x);
        double d2 = diff(y);

        if (d1 == d2) return 0;
        else if (d1 > d2) return -1;
        return 1;
    }
}

class ClosestFactoryImpl : ClosestFactory
{
    public IComparer<BaseNode> makeComparer(double goal)
    {
        return new Closest(goal);
    }
}

class Closest : IComparer<BaseNode>
{
    double prefferedDuration;

    public Closest(double prefferedDuration)
    {
        this.prefferedDuration = prefferedDuration;
    }

    private double diff(BaseNode x)
    {
        return Math.Abs(prefferedDuration - x.totalDuration);
    }

    public int Compare(BaseNode x, BaseNode y)
    {
        double d1 = diff(x);
        double d2 = diff(y);

        if (d1 == d2) return 0;
        else if (d1 > d2) return -1;
        return 1;
    }
}

class Node : BaseNode
{
    public double totalDuration { get; }

    public LevelModule module { get; }

    public int Index { get; set; }

    public int depth { get; private set; }

    public BaseNode parent { get; set; }
    public List<BaseNode> children { get; private set; }

    public List<BaseNode> path { get; set; }

    public Node(double currentTotalDuration, int depth, List<BaseNode> path, LevelModule module)
    {
        this.depth = depth;
        this.module = module;
        this.totalDuration = currentTotalDuration + module.GetDuration();
        this.children = new List<BaseNode> { };
        this.path = new List<BaseNode>(path);
        this.path.Add(this);
    }

    /// Empty node, when module is not required
    public Node(double currentTotalDuration, int depth, List<BaseNode> path)
    {
        this.depth = depth;
        this.totalDuration = currentTotalDuration;
        this.children = new List<BaseNode> { };
        this.path = new List<BaseNode>(path);
        this.path.Add(this);
    }

    public List<BaseNode> getPath()
    {
        List<BaseNode> _path = new List<BaseNode>(this.path);

        // Remove empty nodes from path
        _path.RemoveAll((node) => node.module == null);

        return _path;
    }

    public void AddChildren(List<Node> nodes)
    {
        nodes.ForEach((node) =>
        {
            if (this.children.Find((v) => v == node) == null)
                this.children.Add(node);
        });
    }

    public void GenerateChildren(LevelModuleData nextModule)
    {
        List<Node> children = nextModule.GenerateChildren(this);

        if (nextModule.GetIsRequired() == false)
        {
            // Add empty node, so module will be skipped
            children.Add(new Node(this.totalDuration, this.depth + 1, this.path));
        }

        this.AddChildren(children);
    }
}


static class Extensions
{
    public static List<Node> GenerateChildren(this LevelModuleData module, BaseNode parent)
    {
        List<Node> children = new List<Node> { };

        new List<LevelModule> { module.GetShortVersion(), module.GetLongVersion() }.ForEach((_module) =>
        {
            if (_module != null)
            {
                children.Add(new Node(parent.totalDuration, parent.depth + 1, parent.path, _module));
            }
        });

        return children;
    }

    public static List<LevelModule> MapToLevel(this BaseNode node)
    {
        return node.getPath().ConvertAll((item) =>
        {
            return item.module;
        });
    }
}

interface Solver
{
    List<LevelModule> solve(double prefferedDuration, List<LevelModuleData> modules);
}

interface RootNodeFactory
{
    BaseNode makeRootNode();
}

interface BaseNode : IIndexedObject
{
    double totalDuration { get; }
    LevelModule module { get; }
    int depth { get; }
    List<BaseNode> path { get; }
    BaseNode parent { get; set; }
    List<BaseNode> children { get; }

    void GenerateChildren(LevelModuleData nextModule);
    List<BaseNode> getPath();
}

class NodeFactory : RootNodeFactory
{
    public BaseNode makeRootNode()
    {
        return new Node(.0, 0, new List<BaseNode> { });
    }
}

interface HeuristicFactory
{
    IComparer<BaseNode> makeHeuristic(double goal);
}

interface ClosestFactory
{
    IComparer<BaseNode> makeComparer(double goal);
}

enum SolverMethod
{
    BFS,
    DFS,
    A_STAR,
}



/// PriorityQueue \\\

interface IIndexedObject
{
    int Index { get; set; }
}

internal class PriorityQueue<T> where T : IIndexedObject
{
    protected List<T> InnerList = new List<T>();
    protected IComparer<T> mComparer;

    public PriorityQueue()
    {
        mComparer = Comparer<T>.Default;
    }

    public PriorityQueue(IComparer<T> comparer)
    {
        mComparer = comparer;
    }

    public PriorityQueue(IComparer<T> comparer, int capacity)
    {
        mComparer = comparer;
        InnerList.Capacity = capacity;
    }

    protected void SwitchElements(int i, int j)
    {
        T h = InnerList[i];
        InnerList[i] = InnerList[j];
        InnerList[j] = h;

        InnerList[i].Index = i;
        InnerList[j].Index = j;
    }

    protected virtual int OnCompare(int i, int j)
    {
        return mComparer.Compare(InnerList[i], InnerList[j]);
    }

    /// <summary>
    /// Push an object onto the PQ
    /// </summary>
    /// <param name="O">The new object</param>
    /// <returns>The index in the list where the object is _now_. This will change when objects are taken from or put onto the PQ.</returns>
    public int Push(T item)
    {
        int p = InnerList.Count, p2;
        item.Index = InnerList.Count;
        InnerList.Add(item); // E[p] = O

        do
        {
            if (p == 0)
                break;
            p2 = (p - 1) / 2;
            if (OnCompare(p, p2) < 0)
            {
                SwitchElements(p, p2);
                p = p2;
            }
            else
                break;
        } while (true);
        return p;
    }

    /// <summary>
    /// Get the smallest object and remove it.
    /// </summary>
    /// <returns>The smallest object</returns>
    public T Pop()
    {
        T result = InnerList[0];
        int p = 0, p1, p2, pn;

        InnerList[0] = InnerList[InnerList.Count - 1];
        InnerList[0].Index = 0;

        InnerList.RemoveAt(InnerList.Count - 1);

        result.Index = -1;

        do
        {
            pn = p;
            p1 = 2 * p + 1;
            p2 = 2 * p + 2;
            if (InnerList.Count > p1 && OnCompare(p, p1) > 0) // links kleiner
                p = p1;
            if (InnerList.Count > p2 && OnCompare(p, p2) > 0) // rechts noch kleiner
                p = p2;

            if (p == pn)
                break;
            SwitchElements(p, pn);
        } while (true);

        return result;
    }

    /// <summary>
    /// Notify the PQ that the object at position i has changed
    /// and the PQ needs to restore order.
    /// </summary>
    public void Update(T item)
    {
        int count = InnerList.Count;

        // usually we only need to switch some elements, since estimation won't change that much.
        while ((item.Index - 1 >= 0) && (OnCompare(item.Index - 1, item.Index) > 0))
        {
            SwitchElements(item.Index - 1, item.Index);
        }

        while ((item.Index + 1 < count) && (OnCompare(item.Index + 1, item.Index) < 0))
        {
            SwitchElements(item.Index + 1, item.Index);
        }
    }

    public T RemoveAt(int index)
    {
        T item = InnerList[index];
        InnerList.RemoveAt(index);
        return item;
    }

    /// <summary>
    /// Get the smallest object without removing it.
    /// </summary>
    /// <returns>The smallest object</returns>
    public T Peek()
    {
        if (InnerList.Count > 0)
            return InnerList[0];
        return default(T);
    }

    public void Clear()
    {
        InnerList.Clear();
    }

    public int Count
    {
        get { return InnerList.Count; }
    }
}

