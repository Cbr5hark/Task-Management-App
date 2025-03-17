sig Task {
    belongsTo: one TaskList
}

sig TaskList {
    tasks: set Task,
    belongsTo: one Board
}

sig Board {
    lists: set TaskList
}

fact TaskListContainsItsTasks {
    all t: Task | t in t.belongsTo.tasks
}

fact BoardContainsItsTaskLists {
    all t: TaskList | t in t.belongsTo.lists
}

fact TaskCanOnlyBelongToOneList {
    all t: Task, l1, l2: TaskList |
        t in l1.tasks and t in l2.tasks implies l1 = l2
}

fact OnlyOneBoard {
    #Board = 1
}

pred TaskListContainsTasks() {
    all l: TaskList | some l.tasks
}

assert TaskCanBelongToDifferentLists {
    all t: Task, l1, l2: TaskList |
        t in l1.tasks and t in l2.tasks implies l1 = l2
}

check TaskCanBelongToDifferentLists for 5
run TaskListContainsTasks for 5
