BackendCallbacks
================

 This example demonstrates how to handle asynchronous execution in the back-end, by using singalR.
 
 If signalR is present (and connected), the post handler returns immediately with 202 "Accepted - further processing".
 Then, the back-end code uses signalR to push the result to the cliend, when it is available.

 If signalR is _not_ present, the post handler returns the result directly, but the handler itself uses async/await to run the request in background, asynchronously.

