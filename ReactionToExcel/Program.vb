Imports System.IO
Imports Discord
Imports Discord.WebSocket

Module Program
    Sub Main(args As String())
        MainAsync.GetAwaiter.GetResult()
    End Sub

    Public clientconfig As DiscordSocketConfig = New DiscordSocketConfig With {
        .TotalShards = 1,
        .GatewayIntents = GatewayIntents.All
    }
    Public _client As DiscordShardedClient = New DiscordShardedClient(clientconfig)

    Sub New()
        ' Set console encoding for names with symbols like ♂️ and ♀️
        Console.OutputEncoding = Text.Encoding.UTF8
        ' Set our log, ready, timer, and message received functions
        AddHandler _client.Log, AddressOf LogAsync
        AddHandler _client.MessageReceived, AddressOf MessageReceivedAsync
        AddHandler _client.ShardConnected, AddressOf ShardConnectedAsync
        AddHandler _client.ShardReady, AddressOf ShardReadyAsync
    End Sub

    <STAThread()>
    Public Async Function MainAsync() As Task
        ' Set thread
        ' Threading.Thread.CurrentThread.SetApartmentState(Threading.ApartmentState.STA)

        Colorize("[INFO]      Enter Bot Token:")
        Dim token As String = Console.ReadLine

        Await _client.LoginAsync(TokenType.Bot, token)

        ' Wait for the client to start
        Await _client.StartAsync
        Await Task.Delay(-1)
    End Function

    Private Async Function LogAsync(ByVal log As LogMessage) As Task(Of Task)
        ' Once loginasync and startasync finish we get the log message of "Ready" once we get that, we load everything else
        If log.ToString.Contains("Ready") Then
            Colorize("[GATEWAY]   " & log.ToString)
        ElseIf log.ToString.Contains("gateway") Or log.ToString.Contains("unhandled") Then
        Else
            Colorize("[GATEWAY]   " & log.ToString) ' update console
        End If
        Return Task.CompletedTask
    End Function

    Private Async Function ShardConnectedAsync(ByVal shard As DiscordSocketClient) As Task(Of Task)
        Colorize("[SHARD]     #" & shard.ShardId + 1 & " connected! Guilds: " & shard.Guilds.Count & " Users: " & shard.Guilds.Sum(Function(x) x.MemberCount))
        Return Task.CompletedTask
    End Function

    Private Async Function ShardReadyAsync(ByVal shard As DiscordSocketClient) As Task(Of Task)
        Colorize("[SHARD]     #" & shard.ShardId + 1 & " ready! Guilds: " & shard.Guilds.Count & " Users: " & shard.Guilds.Sum(Function(x) x.MemberCount))
        Return Task.CompletedTask
    End Function

    Private Async Function MessageReceivedAsync(ByVal message As SocketMessage) As Task
        Dim author As SocketGuildUser = message.Author ' Assigns the socket guild user to a variable

        If author.IsBot = False Then ' Checks if the message is from a user
            Dim prefix As String = "rte!" ' Sets the bots prefix for rte!

            If message.Content.StartsWith(prefix) Then ' Checks if the message is a command
                If message.Content.StartsWith(prefix & "export") Then ' Checks if the command is rte!export
                    Try
                        Dim msgFound As Boolean = False
                        Dim msgID As String = message.Content.Replace(prefix & "export ", "") ' Gets just the message ID
                        Dim messages = Await message.Channel.GetMessagesAsync(1000).FlattenAsync() ' Gets cache of the last 1000 messages
                        For i As Integer = 0 To messages.Count - 1 ' Loops through cache
                            If messages(i).Id = msgID Then ' If the message searched is found we execute export
                                msgFound = True ' Message has been found, toggle the boolean
                                Dim emoteReact As Emoji = ":confetti_ball:"
                                Dim reactionusers = Await messages(i).GetReactionUsersAsync(emoteReact, 1000).FlattenAsync ' Gets the list of users that have reacted with this emote
                                Dim csv As String = "Discord UserID,Discord Username" ' Build the CSV file data as a Comma separated string.
                                csv += vbCr & vbLf ' Add line
                                For r As Integer = 0 To reactionusers.Count - 1 ' Loop through reaction users and add a row to csv
                                    csv += reactionusers(r).Id & "," & reactionusers(r).Username & "#" & reactionusers(r).Discriminator ' Add values
                                    csv += vbCr & vbLf ' Add line
                                Next

                                Dim folderPath As String = AppDomain.CurrentDomain.BaseDirectory ' Export path to application startup directory
                                File.WriteAllText(folderPath & "EmoteExport.csv", csv) ' Export file

                                Exit For ' Exit the loop
                            End If
                        Next

                        If msgFound Then ' Check if we were successful
                            Dim builder As EmbedBuilder = New EmbedBuilder
                            builder.WithAuthor("ReactionToExcel", "https://imgur.com/R3nqBnR.png")
                            builder.WithThumbnailUrl("https://imgur.com/R3nqBnR.png")
                            builder.WithColor(219, 172, 69)

                            builder.WithDescription("✔️ Successfully exported reactions to " & AppDomain.CurrentDomain.BaseDirectory & "/EmoteExport.csv")

                            Await message.Channel.SendMessageAsync("", False, builder.Build)
                        Else
                            Dim builder As EmbedBuilder = New EmbedBuilder
                            builder.WithAuthor("ReactionToExcel", "https://imgur.com/R3nqBnR.png")
                            builder.WithThumbnailUrl("https://imgur.com/R3nqBnR.png")
                            builder.WithColor(219, 172, 69)

                            builder.WithDescription("❌ Failed to export reactions.")

                            Await message.Channel.SendMessageAsync("", False, builder.Build)
                        End If
                    Catch ex As Exception
                        Console.WriteLine(ex.ToString)
                    End Try
                End If
            End If
        End If
    End Function

    Public Sub Colorize(ByVal msg As String)
        ' Checks the message for particular string and changes the color, then updates the log
        Select Case True
            Case msg.Contains("ERROR")
                Console.ForegroundColor = ConsoleColor.DarkRed
                Console.WriteLine(msg)
                Console.ResetColor()
            Case msg.Contains("INFO")
                Console.ForegroundColor = ConsoleColor.DarkYellow
                Console.WriteLine(msg)
                Console.ResetColor()
            Case msg.Contains("GATEWAY")
                Console.ForegroundColor = ConsoleColor.DarkMagenta
                Console.WriteLine(msg)
                Console.ResetColor()
            Case Else
                Console.ForegroundColor = ConsoleColor.White
                Console.WriteLine(msg)
                Console.ResetColor()
        End Select
    End Sub
End Module
