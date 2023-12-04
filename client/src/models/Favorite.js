export class Favorite {
    constructor(data) {
        this.id = data.id || data.recipeId
        this.favoriteId = data.favoriteId
        this.creatorId = data.creatorId
        this.accountId = data.accountId

    }
}