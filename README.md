# Roaring Bitmaps Demo (.NET)

This repository demonstrates what roaring bitmaps are and why they are useful for fast set operations at scale.

## What are roaring bitmaps?

A roaring bitmap is a compressed bitmap data structure for storing large sets of integers efficiently.

Instead of storing every value in a regular list or hash set format, roaring bitmaps organize integers into chunks and choose compact internal representations per chunk. This keeps memory usage low while still supporting very fast operations like:

- Intersection (AND)
- Union (OR)
- Difference
- Membership checks
- Cardinality/count queries

Roaring bitmaps are commonly used in analytics, search, recommendation systems, and any workload that does frequent set math on large integer IDs.

## How roaring bitmaps work (high-level)

At a high level, roaring bitmaps split 32-bit integers by high bits into containers. For each container, they use the most efficient encoding based on density (for example, sparse arrays for few values and bitmap-style storage for dense ranges).

This hybrid approach provides:

- Better compression than plain bitsets for sparse data
- Faster operations than many general-purpose collections when doing bulk set math
- Predictable performance for large-scale integer sets

## About this demo

The demo project (`RoarDemo/Program.cs`) compares three approaches for finding overlap between two large user-ID sets:

1. `List<int>` intersection using `Where(...Contains(...))`
2. `HashSet<int>` intersection using `IntersectWith`
3. `Roaring32Bitmap` intersection using `And`

The sample creates two ranges of one million IDs each with overlap, times each operation, and prints the result count and elapsed time.

This helps show the practical performance characteristics of roaring bitmaps for large-scale set operations in .NET.

## Package used

This demo uses:

- [`Roaring.Net`](https://www.nuget.org/packages/Roaring.Net/) (`1.4.0`)

## Video

Related video:

- https://youtu.be/5KpuaGdVTjo?si=oppz95mhUeE6AKR_
