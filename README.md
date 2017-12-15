# VRBaseRaycaster v1.0.0
VRでよく使う視点選択機能を使いやすくまとめたスクリプトです。


# 使い方
#### 初期設定
VABaseRaycaster/Prefabs/VRBaseRaycasterをシーンへ追加します。
追加したVRBaseRaycasterのInspector中の**PlayerHead**へ視線の起点となるオブジェクトを追加します。
**Target Layer**へRaycastの判定を取る対象のLayerを設定します。

#### 注視対象の設定
注視対象にしたいオブジェクトにはColliderと、**VRBaseEventTrigger**コンポーネントを追加します。
VRBaseEventTriggerには4つのイベントハンドラが用意されています。
**- OnEnter**
視点が対象物と衝突した時に呼び出されます。

**- OnHover**
視点が対象物と衝突しつつけている間呼び出されます。

**- OnExit**
視点が対象物から外れた時に呼び出されます。

**- OnProcess**
対象物を注視し続け、**VRBaseRaycast**の**Fixed Process Time**に設定された時間経過した場合に呼び出されます。
視点による選択を実行する場合に使用します。

## ビルド環境
Unity 2017.2.0p4<br>
macOS High Sierra 10.13.2
